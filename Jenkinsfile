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
                    sh 'chmod +x ./ci/01-build.bat'
                    sh './ci/01-build.bat'
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
        stage('ZAP'){
            steps {
                dir('./') {
                    docker compose up -d
                    docker container run -v ./:/zap/wrk/:rw -t --rm --name zap zaproxy/zap-stable zap-baseline.py -t http://192.168.11.114 -g gen.conf -r testreport.html
                    docker compose down
            }
        }
        stage('Email ZAP Report'){
            steps{
                dir('./') {
                    emailext (
                        attachLog: true,
                        attachmentsPattern: '**/*.html',
                        body: "Please find the attached report for the latest OWASP ZAP Scan.",
                        recipientProviders: [buildUser()],
                        subject: "OWASP ZAP Report",
                        to: 'royhu@dst-conn.com.tw'
                    )
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