// Allocations API hosting. Owned by the platform team: agents must never change this file.
param location string = resourceGroup().location
param appName string = 'abc-allocations'

resource plan 'Microsoft.Web/serverfarms@2023-12-01' = {
  name: '${appName}-plan'
  location: location
  sku: { name: 'B1' }
}
