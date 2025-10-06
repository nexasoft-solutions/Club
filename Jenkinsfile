pipeline {
  agent any

  environment {
    SSH_USER = 'ubuntu'
    SSH_HOST = '192.168.10.16'
    APP_DIR = '/opt/nexaclub'
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

    stage('Build Docker Image') {
      when {
        expression { currentBuild.currentResult == 'SUCCESS' }
      }
      steps {
        echo "Construyendo imagen con tag: nexasoft/club:${env.VERSION_TAG}"
        sh "docker build -t nexasoft/club:${env.VERSION_TAG} -f docker/Dockerfile ."
      }
    }

    stage('Push Git Tag') {
      steps {
        withCredentials([usernamePassword(credentialsId: 'github-token', usernameVariable: 'GIT_USER', passwordVariable: 'GIT_PASS')]) {
          sh """
            git config user.name "Aldo Robles"
            git config user.email "aldoroblesarana@gmail.com"
            git tag -a ${env.VERSION_TAG} -m "Release ${env.VERSION_TAG}"
            git push https://${GIT_USER}:${GIT_PASS}@github.com/nexasoft-solutions/club.git --tags
          """
        }
      }
    }

    stage('Prepare & Deploy') {
      steps {
        sshagent(['ssh-proxmox-key']) {
          withCredentials([usernamePassword(credentialsId: 'github-token', usernameVariable: 'GIT_USER', passwordVariable: 'GIT_PASS')]) {
            sh """
ssh -o StrictHostKeyChecking=no ${SSH_USER}@${SSH_HOST} << 'EOF'
set -euxo pipefail

# Config vars
export APP_DIR=${APP_DIR}
export GIT_USER=${GIT_USER}
export GIT_PASS=${GIT_PASS}

# 1️⃣ Verificar conectividad con Vault server
echo "🔍 Verificando conectividad con Vault server..."
if ! curl -s --connect-timeout 10 http://192.168.10.17:8200/v1/sys/health >/dev/null; then
    echo "❌ ERROR: No se puede conectar al Vault server en 192.168.10.17:8200"
    exit 1
fi
echo "✅ Vault server está accesible"

# 2️⃣ Eliminar y recrear directorio de la app
sudo rm -rf \$APP_DIR
sudo mkdir -p \$APP_DIR
sudo chown ${SSH_USER}:${SSH_USER} \$APP_DIR
cd \$APP_DIR

# 3️⃣ Clonar repo limpio
git clone https://\$GIT_USER:\$GIT_PASS@github.com/nexasoft-solutions/club.git .

# 4️⃣ Desplegar contenedores
sudo docker compose down --volumes --remove-orphans || true
sudo docker system prune -af || true
sudo docker compose up -d --build

# 5️⃣ Verificar API
echo "⏳ Esperando inicialización de Nexasoft API..."
sleep 30

if docker ps | grep -q nexasoft-api; then
  echo "✅ Nexasoft API está corriendo"
  echo "📋 Logs API:"
  docker logs nexasoft-api --tail 10
else
  echo "❌ Nexasoft API no está corriendo"
  echo "🔍 Investigando error API:"
  docker logs nexasoft-api --tail 20 || true
  docker inspect nexasoft-api | grep -A 10 -B 5 ExitCode || true
  exit 1
fi
EOF
            """
          }
        }
      }
    }
  }

  post {
    success {
      echo '✅ Pipeline ejecutado exitosamente!'
      echo '🌐 Nexasoft API disponible en: http://192.168.10.16:5051'
    }
    failure {
      echo '❌ Pipeline falló. Revisar logs arriba.'
    }
  }
}
