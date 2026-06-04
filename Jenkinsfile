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
                    rsync -av --delete --no-perms --no-owner --no-times -e 'ssh -i /var/lib/jenkins/.ssh/id_rsa -o StrictHostKeyChecking=no -o IdentitiesOnly=yes -F /dev/null' ./publish/ vagrant@192.168.56.10:/opt/rise/
                    ssh -i /var/lib/jenkins/.ssh/id_rsa -o StrictHostKeyChecking=no vagrant@192.168.56.10 "sudo systemctl restart rise"
                '''
            }
        }
    }
}