pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                sh 'dotnet build Rise.sln'
            }
        }
        stage('Test') {
            steps {
                sh 'dotnet test Rise.sln'
            }
        }
        stage('Publish') {
            steps {
                sh 'dotnet publish src/Rise.Server/Rise.Server.csproj -c Release -o ./publish'
            }
        }
        stage('Deploy') {
            steps {
                sh '''
                    rsync -av --delete ./publish/ vagrant@192.168.56.10:/opt/rise/
                    ssh -o StrictHostKeyChecking=no vagrant@192.168.56.10 "sudo systemctl restart rise"
                '''
            }
        }
    }
}