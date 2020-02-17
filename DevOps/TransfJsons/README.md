# JSON File transform

## Overview

Use this task to apply file transformations and variable substitutions on configuration and parameters files.

## File transformations
?? TODO: ??

## Variable substitution

-> At present only JSON file formats are supported for variable substitution.

-> Tokens defined in the target configuration files are updated and then replaced with variable values.

-> Variable substitutions are run after config file transformations.

-> Variable substitution is applied for only the JSON keys predefined in the object hierarchy. It does not create new keys.

### Examples

To substitute JSON variables that are nested or hierarchical, specify them using JSONPath expressions. For example, to replace the value of ConnectionString in the sample below, you must define a variable as Data.DefaultConnection.ConnectionString in the build or release pipeline (or in a stage within the release pipeline).

```
{
  "Data": {
    "DefaultConnection": {
      "ConnectionString": "Server=(localdb)\SQLEXPRESS;Database=MyDB;Trusted_Connection=True"
    }
  }
}
```

> Note: Only custom variables defined in build and release pipelines are used in substitution. Default and system pipeline variables are excluded. If the same variables are defined in the release pipeline and in a stage, the stage-defined variables supersede the pipeline-defined variables.


### Arguments
?? TODO: ??

