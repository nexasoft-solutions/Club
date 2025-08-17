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
            withCredentials([usernamePassword(credentialsId: 'github-token', usernameVariable: 'GIT_USER', passwordVariable: 'GIT_PASS')]) {
                sh """
                ssh -o StrictHostKeyChecking=no ${SSH_USER}@${SSH_HOST} <<EOF
set -euxo pipefail

export APP_DIR=${APP_DIR}
export VAULT_ROLE_ID=${VAULT_ROLE_ID}
export VAULT_SECRET_ID=${VAULT_SECRET_ID}
export GIT_USER=${GIT_USER}
export GIT_PASS=${GIT_PASS}

sudo mkdir -p \$APP_DIR
sudo chown ${SSH_USER}:${SSH_USER} \$APP_DIR
cd \$APP_DIR

if [ -d ".git" ]; then
  git pull
else
  git clone https://\$GIT_USER:\$GIT_PASS@github.com/nexasoft-solutions/agro.git .
fi

echo "Generating vault-agent.hcl..."
cd \$APP_DIR/vault/config
envsubst < vault-agent.template.hcl > vault-agent.hcl

cd \$APP_DIR
sudo docker compose down || true
sudo docker compose up -d --build

EOF
                """
            }
            }
        }
    }
  }

  post {
    failure {
      echo 'Pipeline falló.'
    }
  }
}
