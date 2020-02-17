// 

TfsTeamProjectCollection tfs = new TfsTeamProjectCollection(new Uri(<tfs uri>));

tfs.EnsureAuthenticated();
VersionControlServer vcs = tfs.GetService<VersionControlServer>();
Workspace ws = vcs.CreateWorkspace("DCSCode");    

//"I:temp" is the location of files which needs to be 
  //moved to TFS
int r = ws.PendAdd(@"I:temp");

//=============================================================================
//Get the current workspace
WS = versionControl.GetWorkspace(workspaceName, versionControl.AuthorizedUser);     

//Mapping TFS Server and code generated
WS.Map(tfsServerFolderPath,localWorkingPath);

//Add all files just created to pending change
int NumberOfChange = WS.PendAdd(localWorkingPath,true);
//Get the list of pending changes
PendingChange[] pendings = WS.GetPendingChanges(tfsServerFolderPath,RecursionType.Full);

//Auto check in code to Server
WS.CheckIn(pendings,"CodeSmith Generator - Auto check-in code.");