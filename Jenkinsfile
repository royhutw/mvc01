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
                    sh 'chmod +x ./ci/99-down.bat'
                    sh './ci/99-down.bat'
                }
            }
        }
        stage('ZAP'){
            steps {
                dir('./') {
                    sh '''
                    docker run -dt --name zap zaproxy/zap-stable /bin/bash
                    docker exec zap mkdir /zap/wrk
                    ./ci/02-test.bat
                    docker container run -t --name zap --network container:mvc01 zaproxy/zap-stable zap-baseline.py -t http://192.168.11.114 -r report.html
                    ./ci/99-down.bat
                    docker cp zap:/zap/work/report.html ${WORKSPACE}/report.html
                    docker container stop zap
                    docker container rm zap
                    '''
                }
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