export default interface CreateCharacterSkillRequestModel {
    characterId: string;
    skillId: number;
    abilityMod: number;
    trained: boolean;
    ranks: number;
    miscMod: number;
}