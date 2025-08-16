pipeline {
    agent any

    environment {
        SSH_USER = 'ubuntu'
        SSH_HOST = '192.168.10.16'
        APP_DIR = '/opt/nexasoft'
        API_PORT = '5050'
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
                    def version = "0.0.1"

                    if (parts.size() == 3) {
                        def patch = parts[2].toInteger() + 1
                        version = "${parts[0]}.${parts[1]}.${patch}"
                    }

                    env.VERSION_NUMBER = version
                    env.VERSION_TAG = "v${version}"
                    currentBuild.displayName = env.VERSION_TAG
                }
            }
        }

        stage('Run Tests') {
            steps {
                sh 'dotnet restore'
                sh '''
                    for proj in $(find ./test -name "*.csproj"); do
                        echo "🧪 Testing: $proj"
                        dotnet test "$proj" --no-build
                    done
                '''
            }
        }

        stage('Inject Vault Credentials') {
            steps {
                sh '''
                    mkdir -p vault/secrets
                    echo "$VAULT_ROLE_ID" > vault/secrets/role_id
                    echo "$VAULT_SECRET_ID" > vault/secrets/secret_id
                '''
            }
        }

        stage('Build Docker Image') {
            steps {
                sh "docker build -t nexasoft/backend:${env.VERSION_NUMBER} ."
            }
        }

        stage('Docker Compose Up') {
            steps {
                sh 'docker compose down || true'
                sh 'docker compose up -d --build'
            }
        }

        stage('Push Git Tag') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'github-token', usernameVariable: 'GIT_USERNAME', passwordVariable: 'GIT_PASSWORD')]) {
                    sh """
                        git config user.name "Jenkins CI"
                        git config user.email "jenkins@example.com"
                        git tag -a ${env.VERSION_TAG} -m "Release version ${env.VERSION_TAG}"
                        git push https://${GIT_USERNAME}:${GIT_PASSWORD}@github.com/nexasoft-solutions/backend.git --tags
                    """
                }
            }
        }
    }

    post {
        always {
            sh 'rm -f vault/secrets/role_id vault/secrets/secret_id'
        }
        failure {
            echo '❌ Pipeline fallido'
        }
    }
}
