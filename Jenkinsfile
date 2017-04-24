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
                bat 'nuget restore src/chargeIO.sln'
                bat "\"${tool 'MSBuild'}\" src/chargeIO.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
            }
        }
        stage('Archive') {
            steps {
		        archive 'ProjectName/bin/Release/**'
            }
        }
    }
}
