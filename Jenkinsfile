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
    	stage('Test'){
	    steps{
		bat "cp ../appsettings.json test-chargeio/bin/Debug/netcoreapp1.0/appsettings.json"
		bat "${tool 'dotnet'} run --project test-chargeio/test-chargeio.csproj test-chargeio/bin/Debug/netcoreapp1.0/test-chargeio.dll"
	    }
    	}
        stage('Archive') {
            steps {
	        archive 'ProjectName/bin/Release/**'
            }
        }
    }
}
