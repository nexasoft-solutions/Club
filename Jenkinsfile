pipeline {
  agent any

  environment {
    SSH_USER = 'ubuntu'
    SSH_HOST = '192.168.10.16'
    APP_DIR = '/opt/nexasoft'
    VAULT_ROLE_ID = credentials('vault-role-id')
    VAULT_SECRET_ID = credentials('vault-secret-id')
  }

  stages {
    stage('Checkout') {
      steps {
        checkout scm
      }
    }

    stage('Set Version') {
        steps {
            script {
                def lastTag = sh(script: 'git describe --tags --abbrev=0 || echo v0.0.0', returnStdout: true).trim()
                echo "Último tag encontrado: ${lastTag}"

                def parts = lastTag.replace("v", "").tokenize('.')
                def version = "0.0.1"

                if (parts.size() == 3) {
                    def major = parts[0].toInteger()
                    def minor = parts[1].toInteger()
                    def patch = parts[2].toInteger() + 1
                    version = "${major}.${minor}.${patch}"
                } else {
                    echo "⚠️ Tag con formato inesperado. Usando ${version}"
                }

                // 🔥 Fijate en esto
                env.VERSION_NUMBER = version
                env.VERSION_TAG = "v${version}"

                currentBuild.displayName = env.VERSION_TAG
                currentBuild.description = "Versión: ${env.VERSION_TAG}"

                echo "✅ VERSION_NUMBER = ${env.VERSION_NUMBER}"
                echo "✅ VERSION_TAG    = ${env.VERSION_TAG}"
            }
        }
    }

    stage('Run Tests') {
        steps {
            script {
                echo '🧪 Ejecutando pruebas .NET proyecto por proyecto...'
                sh 'dotnet --version'
                sh 'dotnet restore'

                sh '''
                    set -e
                    for proj in $(find ./test -name "*.csproj"); do
                        echo "🧪 Testing: $proj"
                        dotnet test "$proj" --verbosity normal
                    done
                '''
            }
        }
    }

    stage('Build Docker Image') {
        when {
            expression { currentBuild.currentResult == 'SUCCESS' }
        }
        steps {
            echo "Construyendo imagen con tag: nexasoft/agro:${env.VERSION_TAG}"
            sh "docker build -t nexasoft/agro:${env.VERSION_TAG} -f docker/Dockerfile ."
        }
    }

    stage('Push Git Tag') {
        steps {
            withCredentials([usernamePassword(credentialsId: 'github-token', usernameVariable: 'GIT_USER', passwordVariable: 'GIT_PASS')]) {
                sh """
                    git config user.name "Aldo Robles"
                    git config user.email "aldoroblesarana@gmail.com"
                    git tag -a ${env.VERSION_TAG} -m "Release ${env.VERSION_TAG}"
                    git push https://${GIT_USER}:${GIT_PASS}@github.com/nexasoft-solutions/agro.git --tags
                """
            }
        }
    }

    stage('Prepare & Deploy') {
      steps {
        sshagent(['ssh-proxmox-key']) {
          withEnv([
            "VAULT_ROLE_ID=${VAULT_ROLE_ID}",
            "VAULT_SECRET_ID=${VAULT_SECRET_ID}"
          ]) {
            sh '''
              ssh -o StrictHostKeyChecking=no $SSH_USER@$SSH_HOST << 'EOF'
                set -euxo pipefail

                # Crear el directorio si no existe
                mkdir -p $APP_DIR

                # Clonar si no existe, o hacer pull si ya está
                if [ ! -d "$APP_DIR/.git" ]; then
                  git clone https://github.com/nexasoft-solutions/agro.git $APP_DIR
                else
                  cd $APP_DIR
                  git pull
                fi

                # Generar archivo vault-agent.hcl usando envsubst
                echo "Generating vault-agent.hcl..."
                cd $APP_DIR/vault/config
                export VAULT_ROLE_ID="$VAULT_ROLE_ID"
                export VAULT_SECRET_ID="$VAULT_SECRET_ID"
                envsubst < vault-agent.template.hcl > vault-agent.hcl

                # Levantar los contenedores
                cd $APP_DIR
                docker compose down || true
                docker compose up -d --build
              EOF
            '''
          }
        }
      }
    }

  }

  post {
    failure { echo 'Pipeline falló.' }
  }
}
