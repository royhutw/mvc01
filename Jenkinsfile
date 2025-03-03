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
                dir('./') {
                    dotnet tool install --global dotnet-sonarscanner
                    export PATH="$PATH:/root/.dotnet/tools"
                    dotnet sonarscanner begin /k:"mvc01" /d:sonar.host.url="http://192.168.11.114:9001"  /d:sonar.token="sqa_9b59fdfc44b3ebfdadbe945e3b0bbe7ae2943a59"
                    sh 'chmod +x ./ci/01-build.bat'
                    sh './ci/01-build.bat'
                    dotnet sonarscanner end /d:sonar.token="sqa_9b59fdfc44b3ebfdadbe945e3b0bbe7ae2943a59"
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