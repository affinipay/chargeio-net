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
		bat "${tool 'dotnet'} restore"
		bat "${tool 'dotnet'} build"
            }
        }
        stage('Archive') {
            steps {
	        archive 'ProjectName/bin/Release/**'
            }
        }
    }
}
