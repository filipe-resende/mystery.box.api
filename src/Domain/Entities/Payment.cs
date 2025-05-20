namespace Domain.Entities;

[Table("Payment")]
public class Payment
{

    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// ID da transação gerada pelo Mercado Pago.
    /// </summary>
    public long MercadoPagoPaymentId { get; set; }

    /// <summary>
    /// Referência externa da sua aplicação (ex: ID do pedido ou compra).
    /// </summary>
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Status atual do pagamento (ex: approved, rejected, pending).
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Detalhes específicos do status (ex: accredited, cc_rejected_call_for_authorize).
    /// </summary>
    public string? StatusDetail { get; set; }

    /// <summary>
    /// Data de criação da transação no Mercado Pago.
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Data de aprovação do pagamento.
    /// </summary>
    public DateTime? ApprovedAt { get; set; }

    /// <summary>
    /// Data prevista de liberação do dinheiro para o vendedor.
    /// </summary>
    public DateTime? ReleaseDate { get; set; }

    /// <summary>
    /// Valor total da transação.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Valor líquido recebido após taxas.
    /// </summary>
    public decimal? NetAmount { get; set; }

    /// <summary>
    /// Quantidade de parcelas.
    /// </summary>
    public int Installments { get; set; }

    /// <summary>
    /// ID do método de pagamento (ex: visa, master, pix).
    /// </summary>
    public string? PaymentMethodId { get; set; }

    /// <summary>
    /// Tipo de pagamento (ex: credit_card, pix, boleto).
    /// </summary>
    public string? PaymentTypeId { get; set; }

    /// <summary>
    /// Quatro últimos dígitos do cartão utilizado (caso seja cartão).
    /// </summary>
    public string? CardLastFour { get; set; }

    /// <summary>
    /// Seis primeiros dígitos do cartão (BIN), útil para identificar bandeira.
    /// </summary>
    public string CardFirstSix { get; set; }

    /// <summary>
    /// Nome do titular do cartão.
    /// </summary>
    public string? CardHolderName { get; set; }

    /// <summary>
    /// Email do comprador.
    /// </summary>
    public string? PayerEmail { get; set; }

    /// <summary>
    /// Tipo de documento do comprador (ex: CPF, DNI).
    /// </summary>
    public string? PayerDocumentType { get; set; }

    /// <summary>
    /// Número do documento do comprador.
    /// </summary>
    public string? PayerDocumentNumber { get; set; }

    /// <summary>
    /// Resposta completa do Mercado Pago em formato JSON, para auditoria e debug.
    /// </summary>
    [Column(TypeName = "jsonb")]
    public string? FullResponseJson { get; set; }

    /// <summary>
    /// Data de criação do registro na base de dados.
    /// </summary>
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Cartões Steam vinculados a este pagamento.
    /// </summary>
    public IList<PurchaseHistory> PurchaseHistories { get; set; } = new List<PurchaseHistory>();

    /// <summary>
    /// Usuário associado ao registro na base de dados.
    /// </summary>
    [ForeignKey("User")]
    public Guid? UserId { get; set; }
    public virtual User User { get; set; }
}
