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
                    ./ci/02-test.bat
                    docker run -dt --network mvc01-smoketest_app-net --name zap zaproxy/zap-stable /bin/bash
                    docker container exec zap mkdir /zap/wrk
                    docker container exec zap zap-baseline.py -t http://localhost -r report.html
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