



import ApiResponseModel from "../models/ApiResponseModel";
import ErrorResponseModel from "../models/ErrorResponseModel";
import TokenModel from "../models/TokenModel";
import HttpClient from "../utils/httpService";


export default class ConnectApi{
    public static async LoginAsync(email:string, password: string, url: string) : Promise<ApiResponseModel<TokenModel>> {
        try {
            const details = {
                username: email,
                password: password,
                grant_type: "password",
                client_id: "renia:4ae25ed28196396194f9fd9b3af0a1ae",
                client_secret: "!R3n!@S3cr3t",
              };
              let formBody = [] ;
              for (let property in details) {
                let encodedKey = encodeURIComponent(property);
                let encodedValue = encodeURIComponent(details[property]);
                formBody.push(encodedKey + "=" + encodedValue);
              }
              let stringFormBody= formBody.join("&");
              const response = await fetch(`${url}/connect/token`, {
                method: "POST",
                headers: {
                  "Content-Type": "application/x-www-form-urlencoded;charset=UTF-8",
                },
                body: stringFormBody,
              });
              if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<TokenModel>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<TokenModel>(data, null);
              } 
              else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<TokenModel>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
        } catch (error) {
            return new ApiResponseModel<TokenModel>(null, ErrorResponseModel.NewError("ConnectApi.LoginAsync().Exception", error));;
        }
    }
    public static async UserInfo(token: string, url: string): Promise<ApiResponseModel<any>>{
      try {
        const uri = `${url}/connect/userinfo`;
        const response = await HttpClient.getAsync(token, uri)
        if (response.ok) {
            const data = await response.json();
            if (data === null) {
                return new ApiResponseModel<any>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
            }
            return new ApiResponseModel<any>(data, null);
        }
        else if (response.status == 400 || response.status == 404) {
            const error = response.statusText;
            return new ApiResponseModel<any>(null, ErrorResponseModel.NewErrorMsg(error, error));
        }
        else if (response.status == 401) {
            const error = response.statusText;
            return new ApiResponseModel<any>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
        }
      } catch (error) {
          return new ApiResponseModel<any>(null, ErrorResponseModel.NewError("ConnectApi.UserInfo().Exception", error));;
      }
    }
}