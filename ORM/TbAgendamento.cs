using System;
using System.Collections.Generic;

namespace MaqCenter.ORM;

public partial class TbAgendamento
{
    public int Id { get; set; }

    public DateTime DtHoraAgendamento { get; set; }

    public DateOnly DataAtendimento { get; set; }

    public TimeOnly Horario { get; set; }

    public int FkUsuario { get; set; }

    public int FkServico { get; set; }

    public virtual TbServico FkServicoNavigation { get; set; } = null!;

    public virtual TbUsuario FkUsuarioNavigation { get; set; } = null!;
}
