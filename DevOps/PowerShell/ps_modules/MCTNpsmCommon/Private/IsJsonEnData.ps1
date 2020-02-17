function IsJsonEnData
{
    param ([string]$TypeName)

    Switch ($TypeName) 
    {
        "System.Boolean" {return $true}
        "System.Object[]" {return $true}
        "System.Int16" {return $true}
        "System.Int32" {return $true}
        "System.Int64" {return $true}
        "System.Decimal" {return $true}
        "System.String" {return $true}
        default {return $false}
    }
}
