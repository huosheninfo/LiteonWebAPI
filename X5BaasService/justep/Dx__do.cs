
	
namespace justep	
{	
	using System;
    using com.justep.baas.action;
    using Newtonsoft.Json.Linq;
	using JSONObject = Newtonsoft.Json.Linq.JObject;
	
	public class Dx__do {
		public static IUserActionUtil ActionUtil { set; private get; }

		private static readonly byte[] DbConfigBytes = {123,34,97,99,99,111,117,110,116,34,58,34,106,97,118,97,58,99,111,109,112,47,101,110,118,47,97,99,99,111,117,110,116,34,44,34,116,97,107,101,111,117,116,34,58,34,106,97,118,97,58,99,111,109,112,47,101,110,118,47,116,97,107,101,111,117,116,34,44,34,116,97,111,98,97,111,34,58,34,106,97,118,97,58,99,111,109,112,47,101,110,118,47,116,97,111,98,97,111,34,125};
		private static JSONObject DbConfig
	    {
	        get
	        {
	            return JSONObject.Parse(ActionUtil.NewString(DbConfigBytes, "UTF-8"));
            }
	    }
	    
	    private static void InitActionParams(JObject privateParams, JObject publicParams, JObject @params)
        {
            foreach (var item in privateParams)
            {
                if (@params[item.Key] == null)
                {
                    @params.Add(item.Key, item.Value);
                }
            }
            foreach (var item in publicParams)
            {
                if (@params[item.Key] == null)
                {
                    @params.Add(item.Key, item.Value);
                }
            }
        }
		
		
	
		public static JSONObject DuanXinCheck(JSONObject paramsObject, ActionContext context){
			bool selfContext = null == context;
			if (selfContext)
		    {
		        context = new ActionContext(DbConfig);
		    }
		    
			byte[] privateParamBytes =  {123,125};
			JSONObject privateParams = JSONObject.Parse(ActionUtil.NewString(privateParamBytes, "UTF-8"));
			byte[] publicParamBytes = {123,125};
			JSONObject publicParams = JSONObject.Parse(ActionUtil.NewString(publicParamBytes, "UTF-8"));
			
			InitActionParams(privateParams, publicParams, paramsObject);
				
			try
            {		
				var ret =  justep.Dx.DuanXinCheck(paramsObject, context);
			    if (selfContext)
                {
                    context.commit();
                }
                return ret;
            }
            catch (Exception e)
            {
                try
                {
                    if (selfContext)
                    {
                        context.rollback();
                    }
                }
                catch (Exception)
                {                    
                }
                
                throw new SystemException(e.Message, e);
            }
		}
	
	
	
		public static JSONObject login(JSONObject paramsObject, ActionContext context){
			bool selfContext = null == context;
			if (selfContext)
		    {
		        context = new ActionContext(DbConfig);
		    }
		    
			byte[] privateParamBytes =  {123,125};
			JSONObject privateParams = JSONObject.Parse(ActionUtil.NewString(privateParamBytes, "UTF-8"));
			byte[] publicParamBytes = {123,125};
			JSONObject publicParams = JSONObject.Parse(ActionUtil.NewString(publicParamBytes, "UTF-8"));
			
			InitActionParams(privateParams, publicParams, paramsObject);
				
			try
            {		
				var ret =  justep.Dx.login(paramsObject, context);
			    if (selfContext)
                {
                    context.commit();
                }
                return ret;
            }
            catch (Exception e)
            {
                try
                {
                    if (selfContext)
                    {
                        context.rollback();
                    }
                }
                catch (Exception)
                {                    
                }
                
                throw new SystemException(e.Message, e);
            }
		}
	
	
	
		public static JSONObject saveUser(JSONObject paramsObject, ActionContext context){
			bool selfContext = null == context;
			if (selfContext)
		    {
		        context = new ActionContext(DbConfig);
		    }
		    
			byte[] privateParamBytes =  {123,34,100,98,34,58,34,116,97,107,101,111,117,116,34,44,34,112,101,114,109,105,115,115,105,111,110,115,34,58,123,34,116,97,107,101,111,117,116,95,117,115,101,114,34,58,34,34,125,125};
			JSONObject privateParams = JSONObject.Parse(ActionUtil.NewString(privateParamBytes, "UTF-8"));
			byte[] publicParamBytes = {123,125};
			JSONObject publicParams = JSONObject.Parse(ActionUtil.NewString(publicParamBytes, "UTF-8"));
			
			InitActionParams(privateParams, publicParams, paramsObject);
				
			try
            {		
				var ret =  com.justep.baas.action.CRUD.save(paramsObject, context);
			    if (selfContext)
                {
                    context.commit();
                }
                return ret;
            }
            catch (Exception e)
            {
                try
                {
                    if (selfContext)
                    {
                        context.rollback();
                    }
                }
                catch (Exception)
                {                    
                }
                
                throw new SystemException(e.Message, e);
            }
		}
	
	
	}
}
	
	