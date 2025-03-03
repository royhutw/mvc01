pipeline {
    agent any
    environment {
       REGISTRY = "192.168.11.114:5000"
    }
    stages {
        stage('Verify') {
            steps {
                dir('') {
                    sh 'chmod +x ./ci/00-verify.bat'
                    sh './ci/00-verify.bat'
                }
            }
        }
        stage('Build') {
            steps {
                withSonarQubeEnv('sonarqube') {
                    dir('./') {
                        sh 'chmod +x ./ci/01-build.bat'
                        sh './ci/01-build.bat'
                    }
                }
            }
        }
        stage('Test') {
            steps {
                dir('./') {
                    sh 'chmod +x ./ci/02-test.bat'
                    sh './ci/02-test.bat'
                }
            }
        }
        stage('Push') {
            steps {
                dir('./') {
                    sh 'chmod +x ./ci/03-push.bat'
                    sh './ci/03-push.bat'
                    echo "Pushed web to http://$REGISTRY/v2/it/mvc01/tags/list"
                    echo "Pushed api to http://$REGISTRY/v2/it/mvc01/tags/list"
                }
            }
        }
    }
}