


import ApiResponseModel from "../models/ApiResponseModel";
import CharacterGearModel from "../models/CharacterGearModel";
import ErrorResponseModel from "../models/ErrorResponseModel";
import InsertCharacterGearRequestModel from "../models/InsertCharacterGearRequestModel";
import TransferGearItemRequestModel from "../models/TransferGearItemRequestModel";
import UpdateCharacterGearRequestModel from "../models/UpdateCharacterGearModel";
import { characterGearEndpoint } from "../utils/constants";
import HttpClient from "../utils/httpService";


export default class CharacterGearApi{
    public static async GetAsync(token:string, url: string, characterId: string) : Promise<ApiResponseModel<Array<CharacterGearModel>>> {
        try {
            const uri = `${url}/${characterGearEndpoint}/${characterId}/all`;
            const response = await HttpClient.getAsync(token, uri)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<CharacterGearModel[]>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<CharacterGearModel[]>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<CharacterGearModel[]>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<CharacterGearModel[]>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<CharacterGearModel[]>(null, ErrorResponseModel.NewError("CharacterGearApi.GetAsync().Exception", error));;
        }
    }
    public static async GetByIdAsync(token:string, url: string, id: number) : Promise<ApiResponseModel<CharacterGearModel>> {
        try {
            const uri = `${url}/${characterGearEndpoint}/${id}`;
            const response = await HttpClient.getAsync(token, uri)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<CharacterGearModel>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<CharacterGearModel>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<CharacterGearModel>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<CharacterGearModel>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<CharacterGearModel>(null, ErrorResponseModel.NewError("CharacterGearApi.GetByIdAsync().Exception", error));;
        }
    }
    public static async GetMoneyAsync(token:string, url: string, characterId: string) : Promise<ApiResponseModel<CharacterGearModel>> {
        try {
            const uri = `${url}/${characterGearEndpoint}/${characterId}/money`;
            const response = await HttpClient.getAsync(token, uri)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<CharacterGearModel>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<CharacterGearModel>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<CharacterGearModel>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<CharacterGearModel>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<CharacterGearModel>(null, ErrorResponseModel.NewError("CharacterGearApi.GetMoneyAsync().Exception", error));;
        }
    }
    public static async InsertAsync(token:string, url: string, request: InsertCharacterGearRequestModel) : Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${characterGearEndpoint}`;
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
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("CharacterGearApi.InsertAsync().Exception", error));;
        }
    }
   
    public static async UpdateAsync(token:string, url: string, request: UpdateCharacterGearRequestModel) : Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${characterGearEndpoint}`;
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
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("CharacterGearApi.UpdateAsync().Exception", error));;
        }
    }
    public static async TransferItemAsync(token:string, url: string, request: TransferGearItemRequestModel) : Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${characterGearEndpoint}/transfer`;
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
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("CharacterGearApi.TransferItemAsync().Exception", error));;
        }
    }
    public static async DeleteAsync(token:string, url: string, id: number) : Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${characterGearEndpoint}/${id}/delete`;
            const response = await HttpClient.deleteAsync(token, uri)
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
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("CharacterGearApi.DeleteAsync().Exception", error));;
        }
    }
}