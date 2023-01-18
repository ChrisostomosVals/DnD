

import ApiResponseModel from "../models/ApiResponseModel";
import SkillModel from "../models/SkillModel";
import ErrorResponseModel from "../models/ErrorResponseModel";
import { skillEndpoint } from "../utils/constants";
import HttpClient from "../utils/httpService";



export default class SkillApi{
    public static async GetAsync(token:string, url: string) : Promise<ApiResponseModel<Array<SkillModel>>> {
        try {
            const uri = `${url}/${skillEndpoint}`;
            const response = await HttpClient.getAsync(token, uri)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<SkillModel[]>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<SkillModel[]>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<SkillModel[]>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<SkillModel[]>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<SkillModel[]>(null, ErrorResponseModel.NewError("SkillApi.GetAsync().Exception", error));;
        }
    }
    public static async GetByIdAsync(token:string, url: string, id: number) : Promise<ApiResponseModel<SkillModel>> {
        try {
            const uri = `${url}/${skillEndpoint}/${id}`;
            const response = await HttpClient.getAsync(token, uri)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<SkillModel>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<SkillModel>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<SkillModel>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<SkillModel>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<SkillModel>(null, ErrorResponseModel.NewError("SkillApi.GetByIdAsync().Exception", error));;
        }
    }
}