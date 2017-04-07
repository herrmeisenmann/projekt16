pipeline {
  agent any
  stages {
    stage('REstore Stuf') {
      steps {
        bat 'git submodule update --init'
        bat '${env.NUGET_EXE} restore Server.sln"'
      }
    }
    stage('Build') {
      steps {
        tool(name: 'x64-v15.0', type: 'MSBuild')
      }
    }
    stage('') {
      steps {
        echo '???'
      }
    }
  }
}