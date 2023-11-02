namespace SuggestionAppLibrary.Models;

public class BasicUserModel
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string DisplayName { get; set; }

    public BasicUserModel()
    {
        
    }
    // to convert full object(UserModel) to basic object(BasicUserModel)
    public BasicUserModel(UserModel user)
    {
        Id = user.Id;
        DisplayName = user.DisplayName;
    }
}
