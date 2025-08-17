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
          def parts = lastTag.replace("v", "").tokenize('.')
          def version = (parts.size() == 3) ?
            "${parts[0]}.${parts[1]}.${parts[2].toInteger() + 1}" : "0.0.1"
          env.VERSION_TAG = "v${version}"
          currentBuild.displayName = env.VERSION_TAG
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
                        dotnet test "$proj" --no-build --verbosity normal
                    done
                '''
            }
        }
    }

    stage('Build Docker Image') {
      steps {
        sh "docker build -t nexasoft/agro:${VERSION_TAG} ."
      }
    }

    stage('Push Git Tag') {
      steps {
        withCredentials([usernamePassword(credentialsId: 'github-token', usernameVariable: 'GIT_USER', passwordVariable: 'GIT_PASS')]) {
          sh """
            git config user.name "Jenkins CI"
            git config user.email "jenkins@example.com"
            git tag -a ${VERSION_TAG} -m "Release ${VERSION_TAG}"
            git push https://${GIT_USER}:${GIT_PASS}@github.com/nexasoft-solutions/agro.git --tags
          """
        }
      }
    }

    stage('Prepare & Deploy') {
      steps {
        sshagent(['ssh-proxmox-key']) {
          sh """
            ssh -o StrictHostKeyChecking=no ${SSH_USER}@${SSH_HOST} << EOF
              set -euxo pipefail
              cd ${APP_DIR}
              git pull || git clone https://github.com/nexasoft-solutions/agro.git ${APP_DIR}

              echo "Generating vault-agent.hcl..."
              cd ${APP_DIR}/vault/config
              export VAULT_ROLE_ID='${VAULT_ROLE_ID}'
              export VAULT_SECRET_ID='${VAULT_SECRET_ID}'
              envsubst < vault-agent.template.hcl > vault-agent.hcl

              cd ${APP_DIR}
              docker compose down || true
              docker compose up -d --build
            EOF
          """
        }
      }
    }
  }

  post {
    failure { echo 'Pipeline falló.' }
  }
}
