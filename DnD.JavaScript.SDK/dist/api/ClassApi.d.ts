import ApiResponseModel from "../models/ApiResponseModel";
import ClassModel from "../models/ClassModel";
export default class ClassApi {
    static GetAsync(token: string, url: string): Promise<ApiResponseModel<Array<ClassModel>>>;
    static GetByIdAsync(token: string, url: string, id: number): Promise<ApiResponseModel<ClassModel>>;
    static GetByCategoryIdAsync(token: string, url: string, categoryId: number): Promise<ApiResponseModel<ClassModel[]>>;
}
