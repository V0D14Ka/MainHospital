using System.Text.Json.Serialization;
namespace MainHospital.Views;


/// <summary>
/// Вьюха для ответа на запрос получения пользователя
/// </summary>
/// <remarks>
/// Атрибут JsonPropertyName нужен для сериализации или десериализации сообщений в формате джсон
/// Простым словами, эти атрибуты помогают сконвертить класс в джсон с указанными в них названиями полей
/// И, наоборот, десериализовать/распарсить джсон обратно в объект, в соответствии с названиями полей
/// </remarks>
public class SheduleSearchView
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("doctorId")]
    public int DoctorId { get; set; }

    [JsonPropertyName("startworking")]
    public DateTime StartWorking { get; set; }

    [JsonPropertyName("endworking")]
    public DateTime EndWorking { get; set; }
}