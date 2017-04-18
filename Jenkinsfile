pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
		        checkout scm
            }
        }
        stage('Build'){
            steps {
                bat 'nuget restore SolutionName.sln'
                bat "\"${tool 'MSBuild'}\" chargeIO.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
            }
        }
        stage('Archive') {
            steps {
		        archive 'ProjectName/bin/Release/**'
            }
        }
    }
}