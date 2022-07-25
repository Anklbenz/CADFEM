using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ServiceRequestsData;
using ThingData;
using UnityEngine;

public class ThingWorksServices : WebRequestSender{
    public ThingWorksServices(string servicesUrl, string authorizationHeader) : base(servicesUrl, authorizationHeader){
    }
    
    private async UniTask<Operation[]> GetOperations(WorkLogIdParam idParam){
        var operations = await GetServiceResult<Operations, WorkLogIdParam>(GET_OPERATIONS_SERVICE, idParam);
        return operations != null ? operations.rows : new Operation[] { new Operation() };
    }
    
    
    private WorkLogIdParam CreateWorkLogIdParam(int workLogId) => new() { work_log_id = workLogId };
}
