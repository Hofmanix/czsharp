DBVERSION=$(<migrationversion.txt)
DBVERSION=$((DBVERSION+1))
echo "$DBVERSION"
echo "$DBVERSION" > migrationversion.txt
docker exec dockercompose_czsharp_1 dotnet ef migrations add migration$DBVERSION --msbuildprojectextensionspath ../obj
docker exec dockercompose_czsharp_1 dotnet ef database update --msbuildprojectextensionspath ../obj
