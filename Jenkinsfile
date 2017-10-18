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
		bat "cp ../appsettings.json ChargeIo.Tests/bin/Debug/netcoreapp2.0/appsettings.json"
		bat "${tool 'dotnet'} run --project ChargeIo.Tests/ChargeIo.Tests.csproj ChargeIo.Tests/bin/Debug/netcoreapp2.0/ChargeIo.Tests.dll"
	    }
    	}
        stage('Archive') {
            steps {
	        archive 'ProjectName/bin/Release/**'
            }
        }
    }
}
