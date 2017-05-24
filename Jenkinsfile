pipeline {
    agent {
        node {
            label 'msbuild'
        }
    }

    stages {
        stage('Checkout') {
            steps {
		        checkout scm
            }
        }
        stage('Build'){
            steps {
                bat 'dotnet restore'
		bat 'dotnet build'
            }
        }
        stage('Archive') {
            steps {
	        archive 'ProjectName/bin/Release/**'
            }
        }
    }
}
