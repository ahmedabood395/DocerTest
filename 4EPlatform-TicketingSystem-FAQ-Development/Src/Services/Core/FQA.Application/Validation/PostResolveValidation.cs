namespace FAQ.Application.Validation
{
    public class PostResolveValidation : AbstractValidator<ResolveReport>
    {
        public PostResolveValidation()
        {
            RuleFor(x => x.ResolveName).NotEmpty().WithMessage("ResolveName is required");
            RuleFor(x => x.ResolveDescription).NotEmpty().WithMessage("ResolveDescription is required");
            RuleFor(x => x.MainClassificationId).NotNull().WithMessage("MainClassificationId is required");
            RuleFor(x => x.ServiceCatalogId).NotNull().WithMessage("ServiceCatalogId is required");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("ImageUrl is required");
        }
    }
}
