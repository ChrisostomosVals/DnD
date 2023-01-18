

import ApiResponseModel from "../models/ApiResponseModel";
import LocationEventModel from "../models/LocationEventModel";
import ErrorResponseModel from "../models/ErrorResponseModel";
import { locationEventEndpoint } from "../utils/constants";
import HttpClient from "../utils/httpService";
import InsertLocationEventRequestModel from "../models/InsertLocationEventRequestModel";
import UpdateLocationEventRequestModel from "../models/UpdateLocationEventRequestModel";



export default class LocationEventApi {
    public static async GetAsync(token: string, url: string): Promise<ApiResponseModel<LocationEventModel[]>> {
        try {
            const uri = `${url}/${locationEventEndpoint}/all`;
            const response = await HttpClient.getAsync(token, uri)
            if (response.ok) {
                const data = await response.json();
                if (data === null) {
                    return new ApiResponseModel<LocationEventModel[]>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<LocationEventModel[]>(data, null);
            }
            else if (response.status == 400 || response.status == 404) {
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<LocationEventModel[]>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401) {
                const error = response.statusText;
                return new ApiResponseModel<LocationEventModel[]>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<LocationEventModel[]>(null, ErrorResponseModel.NewError("LocationEventApi.GetAsync().Exception", error));;
        }
    }
    public static async GetByIdAsync(token: string, url: string, id: number): Promise<ApiResponseModel<LocationEventModel>> {
        try {
            const uri = `${url}/${locationEventEndpoint}/${id}`;
            const response = await HttpClient.getAsync(token, uri)
            if (response.ok) {
                const data = await response.json();
                if (data === null) {
                    return new ApiResponseModel<LocationEventModel>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<LocationEventModel>(data, null);
            }
            else if (response.status == 400 || response.status == 404) {
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<LocationEventModel>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401) {
                const error = response.statusText;
                return new ApiResponseModel<LocationEventModel>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<LocationEventModel>(null, ErrorResponseModel.NewError("LocationEventApi.GetByIdAsync().Exception", error));;
        }
    }
    public static async CreateAsync(token: string, url: string, request: InsertLocationEventRequestModel): Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${locationEventEndpoint}`;
            const response = await HttpClient.postAsync(token, uri, request)
            if (response.ok) {
                const data = await response.json();
                if (data === null) {
                    return new ApiResponseModel<void>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<void>(data, null);
            }
            else if (response.status == 400 || response.status == 404) {
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401) {
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("LocationEventApi.CreateAsync().Exception", error));;
        }
    }
    public static async UpdateAsync(token: string, url: string, request: UpdateLocationEventRequestModel): Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${locationEventEndpoint}`;
            const response = await HttpClient.putAsync(token, uri, request)
            if (response.ok) {
                const data = await response.json();
                if (data === null) {
                    return new ApiResponseModel<void>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<void>(data, null);
            }
            else if (response.status == 400 || response.status == 404) {
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401) {
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("LocationEventApi.UpdateAsync().Exception", error));;
        }
    }
    public static async DeleteAsync(token: string, url: string, id: number): Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${locationEventEndpoint}/${id}/delete`;
            const response = await HttpClient.deleteAsync(token, uri)
            if (response.ok) {
                const data = await response.json();
                if (data === null) {
                    return new ApiResponseModel<void>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<void>(data, null);
            }
            else if (response.status == 400 || response.status == 404) {
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401) {
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("LocationEventApi.DeleteAsync().Exception", error));;
        }
    }
}