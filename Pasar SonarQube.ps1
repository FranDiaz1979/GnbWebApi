cls

cd "E:\Proyectos\francisco-diaz-guerrero\src\"

echo ""
echo "Pasando SonarQube"
echo "=================="

"E:\sonar-scanner-msbuild-5.0.4.24009-net46\SonarScanner.MSBuild.exe" begin /k:"GnbWebApi" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="6685ceb1269ec2de5d6b85b46039f3edaf803b70"

"C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MsBuild.exe" /t:Rebuild

"E:\sonar-scanner-msbuild-5.0.4.24009-net46\SonarScanner.MSBuild.exe" end /d:sonar.login="6685ceb1269ec2de5d6b85b46039f3edaf803b70"

echo "Pulse enter para cerrar la ventana"

pause