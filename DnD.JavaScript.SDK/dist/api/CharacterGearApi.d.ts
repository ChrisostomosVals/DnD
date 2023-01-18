import ApiResponseModel from "../models/ApiResponseModel";
import CharacterGearModel from "../models/CharacterGearModel";
import InsertCharacterGearRequestModel from "../models/InsertCharacterGearRequestModel";
import TransferGearItemRequestModel from "../models/TransferGearItemRequestModel";
import UpdateCharacterGearRequestModel from "../models/UpdateCharacterGearModel";
export default class CharacterGearApi {
    static GetAsync(token: string, url: string, characterId: string): Promise<ApiResponseModel<Array<CharacterGearModel>>>;
    static GetByIdAsync(token: string, url: string, id: number): Promise<ApiResponseModel<CharacterGearModel>>;
    static GetMoneyAsync(token: string, url: string, characterId: string): Promise<ApiResponseModel<CharacterGearModel>>;
    static InsertAsync(token: string, url: string, request: InsertCharacterGearRequestModel): Promise<ApiResponseModel<void>>;
    static UpdateAsync(token: string, url: string, request: UpdateCharacterGearRequestModel): Promise<ApiResponseModel<void>>;
    static TransferItemAsync(token: string, url: string, request: TransferGearItemRequestModel): Promise<ApiResponseModel<void>>;
    static DeleteAsync(token: string, url: string, id: number): Promise<ApiResponseModel<void>>;
}
