import ApiResponseModel from "../models/ApiResponseModel";
import SkillModel from "../models/SkillModel";
export default class SkillApi {
    static GetAsync(token: string, url: string): Promise<ApiResponseModel<Array<SkillModel>>>;
    static GetByIdAsync(token: string, url: string, id: number): Promise<ApiResponseModel<SkillModel>>;
}
