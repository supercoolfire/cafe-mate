```powershell
# init-phase1.ps1
# Run this inside cafe-mate\internet-cafe-system\

Write-Host "🚀 Initializing Internet Cafe Management System Phase 1..."

# Ensure we're in the right folder
if (-Not (Test-Path ".\InternetCafe.sln")) {
    Write-Host "🔧 Creating solution file..."
    dotnet new sln -n InternetCafe
} else {
    Write-Host "✅ Solution file already exists."
}

# Create server-app project if not exists
if (-Not (Test-Path ".\server-app\server-app.csproj")) {
    Write-Host "📦 Creating server-app (WinForms)..."
    dotnet new winforms -n server-app -o server-app
    dotnet sln InternetCafe.sln add server-app\server-app.csproj
} else {
    Write-Host "✅ server-app project already exists."
}

# Create client-app project if not exists
if (-Not (Test-Path ".\client-app\client-app.csproj")) {
    Write-Host "📦 Creating client-app (WinForms)..."
    dotnet new winforms -n client-app -o client-app
    dotnet sln InternetCafe.sln add client-app\client-app.csproj
} else {
    Write-Host "✅ client-app project already exists."
}

Write-Host "🔨 Building solution..."
dotnet build InternetCafe.sln

Write-Host "`n🎉 Phase 1 initialization complete! You can now open InternetCafe.sln in Visual Studio or run projects with dotnet run."
```
