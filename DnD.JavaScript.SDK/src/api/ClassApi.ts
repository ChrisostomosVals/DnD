

import ApiResponseModel from "../models/ApiResponseModel";
import ClassModel from "../models/ClassModel";
import ErrorResponseModel from "../models/ErrorResponseModel";
import { classEndpoint } from "../utils/constants";
import HttpClient from "../utils/httpService";



export default class ClassApi{
    public static async GetAsync(token:string, url: string) : Promise<ApiResponseModel<Array<ClassModel>>> {
        try {
            const uri = `${url}/${classEndpoint}`;
            const response = await HttpClient.getAsync(token, uri)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<ClassModel[]>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<ClassModel[]>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<ClassModel[]>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<ClassModel[]>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<ClassModel[]>(null, ErrorResponseModel.NewError("ClassApi.GetAsync().Exception", error));;
        }
    }
    public static async GetByIdAsync(token:string, url: string, id: number) : Promise<ApiResponseModel<ClassModel>> {
        try {
            const uri = `${url}/${classEndpoint}/${id}`;
            const response = await HttpClient.getAsync(token, uri)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<ClassModel>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<ClassModel>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<ClassModel>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<ClassModel>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<ClassModel>(null, ErrorResponseModel.NewError("ClassApi.GetByIdAsync().Exception", error));;
        }
    }
    public static async GetByCategoryIdAsync(token:string, url: string, categoryId: number) : Promise<ApiResponseModel<ClassModel[]>> {
        try {
            const uri = `${url}/${classEndpoint}/${categoryId}/category`;
            const response = await HttpClient.getAsync(token, uri)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<ClassModel[]>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<ClassModel[]>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<ClassModel[]>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<ClassModel[]>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<ClassModel[]>(null, ErrorResponseModel.NewError("ClassApi.GetByCategoryIdAsync().Exception", error));;
        }
    }
}