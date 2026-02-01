namespace AwsSocialMedia.Insfrastructure.Validators
{
    using Core.DTOs;
    using FluentValidation;

    public class PostDtoValidator : AbstractValidator<PostCreateDto>
    {
        public PostDtoValidator()
        {
            RuleFor(post => post.UserId)
                .NotEmpty().WithMessage("UserId es requerido")
                .GreaterThan(0).WithMessage("UserId debe ser mayor que 0")
                .NotNull();

            RuleFor(post => post.Description)
                .NotEmpty().WithMessage("Descripción es requerido")
                .NotNull()
                .Length(10, 500);

            RuleFor(x => x.Image)
                .MaximumLength(500).WithMessage("La URL de la imagen no puede exceder 500 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Image));
        }
    }
}
