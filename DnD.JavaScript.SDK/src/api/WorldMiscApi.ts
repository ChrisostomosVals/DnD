

import ApiResponseModel from "../models/ApiResponseModel";
import WorldMiscModel from "../models/WorldMiscModel";
import ErrorResponseModel from "../models/ErrorResponseModel";
import { worldMiscEndpoint } from "../utils/constants";
import HttpClient from "../utils/httpService";
import InsertWorldMiscRequestModel from "../models/InsertWorldMiscRequestModel";
import UpdateWorldMiscRequestModel from "../models/UpdateWorldMiscRequestModel";



export default class WorldMiscApi{
    public static async GetAsync(token:string, url: string) : Promise<ApiResponseModel<Array<WorldMiscModel>>> {
        try {
            const uri = `${url}/${worldMiscEndpoint}`;
            const response = await HttpClient.getAsync(token, uri)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<WorldMiscModel[]>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<WorldMiscModel[]>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<WorldMiscModel[]>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<WorldMiscModel[]>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<WorldMiscModel[]>(null, ErrorResponseModel.NewError("WorldMiscApi.GetAsync().Exception", error));;
        }
    }
    public static async GetByIdAsync(token:string, url: string, id: number) : Promise<ApiResponseModel<WorldMiscModel>> {
        try {
            const uri = `${url}/${worldMiscEndpoint}/${id}`;
            const response = await HttpClient.getAsync(token, uri)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<WorldMiscModel>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<WorldMiscModel>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<WorldMiscModel>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<WorldMiscModel>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<WorldMiscModel>(null, ErrorResponseModel.NewError("WorldMiscApi.GetByIdAsync().Exception", error));;
        }
    }
    public static async GetByDependIdAsync(token:string, url: string, dependId: number) : Promise<ApiResponseModel<WorldMiscModel>> {
        try {
            const uri = `${url}/${worldMiscEndpoint}/${dependId}/depend`;
            const response = await HttpClient.getAsync(token, uri)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<WorldMiscModel>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<WorldMiscModel>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<WorldMiscModel>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<WorldMiscModel>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<WorldMiscModel>(null, ErrorResponseModel.NewError("WorldMiscApi.GetByDependIdAsync().Exception", error));;
        }
    }
    public static async CreateAsync(token:string, url: string, request: InsertWorldMiscRequestModel) : Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${worldMiscEndpoint}`;
            const response = await HttpClient.postAsync(token, uri, request)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<void>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<void>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("WorldMiscApi.CreateAsync().Exception", error));;
        }
    }
    public static async UpdateAsync(token:string, url: string, request: UpdateWorldMiscRequestModel) : Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${worldMiscEndpoint}`;
            const response = await HttpClient.putAsync(token, uri, request)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<void>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<void>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("WorldMiscApi.UpdateAsync().Exception", error));;
        }
    }
}