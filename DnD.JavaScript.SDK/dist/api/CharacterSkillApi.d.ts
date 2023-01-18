import ApiResponseModel from "../models/ApiResponseModel";
import CharacterSkillModel from "../models/CharacterSkillModel";
import CreateCharacterSkillRequestModel from "../models/CreateCharacterSkillRequestModel";
import UpdateCharacterSkillRequestModel from "../models/UpdateCharacterSkillRequestModel";
export default class CharacterSkillApi {
    static GetAsync(token: string, url: string, characterId: string): Promise<ApiResponseModel<Array<CharacterSkillModel>>>;
    static GetByIdAsync(token: string, url: string, id: number): Promise<ApiResponseModel<CharacterSkillModel>>;
    static CreateAsync(token: string, url: string, request: CreateCharacterSkillRequestModel): Promise<ApiResponseModel<void>>;
    static UpdateAsync(token: string, url: string, request: UpdateCharacterSkillRequestModel): Promise<ApiResponseModel<void>>;
}
