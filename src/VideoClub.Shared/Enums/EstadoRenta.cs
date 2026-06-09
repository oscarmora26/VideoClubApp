using System.Text.Json.Serialization;

namespace VideoClub.Shared.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EstadoRenta
{
    Activa,
    Devuelta
}
