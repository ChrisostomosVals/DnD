import ApiResponseModel from "../models/ApiResponseModel";
import LocationEventModel from "../models/LocationEventModel";
import InsertLocationEventRequestModel from "../models/InsertLocationEventRequestModel";
import UpdateLocationEventRequestModel from "../models/UpdateLocationEventRequestModel";
export default class LocationEventApi {
    static GetAsync(token: string, url: string): Promise<ApiResponseModel<LocationEventModel[]>>;
    static GetByIdAsync(token: string, url: string, id: number): Promise<ApiResponseModel<LocationEventModel>>;
    static CreateAsync(token: string, url: string, request: InsertLocationEventRequestModel): Promise<ApiResponseModel<void>>;
    static UpdateAsync(token: string, url: string, request: UpdateLocationEventRequestModel): Promise<ApiResponseModel<void>>;
    static DeleteAsync(token: string, url: string, id: number): Promise<ApiResponseModel<void>>;
}
