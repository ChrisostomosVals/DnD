import ApiResponseModel from "../models/ApiResponseModel";
import RaceModel from "../models/RaceModel";
export default class RaceApi {
    static GetAsync(token: string, url: string): Promise<ApiResponseModel<Array<RaceModel>>>;
    static GetByIdAsync(token: string, url: string, id: number): Promise<ApiResponseModel<RaceModel>>;
    static GetByCategoryIdAsync(token: string, url: string, categoryId: number): Promise<ApiResponseModel<RaceModel[]>>;
}
