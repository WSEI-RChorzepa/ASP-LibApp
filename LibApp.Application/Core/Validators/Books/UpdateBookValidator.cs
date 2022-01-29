using FluentValidation;
using LibApp.Application.UseCases.Books.Commands;

namespace LibApp.Application.Core.Validators
{
    public class UpdateBookValidator : AbstractValidator<UpdateBook.Command>
    {
        public UpdateBookValidator()
        {
            RuleFor(a=>a.Id)
                .NotEmpty().WithMessage("Pole 'ID' jest wymagane.");

            RuleFor(a => a.Name)
               .NotEmpty().WithMessage("Pole 'Nazwa' jest wymagane.");

            RuleFor(a => a.AuthorName)
                .NotEmpty().WithMessage("Pole 'Autor' jest wymagane.");

            RuleFor(a => a.GenreId)
                .NotEmpty().WithMessage("Pole 'Gatunek' jest wymagane.");

            RuleFor(a => a.NumberInStock)
                .NotEmpty().WithMessage("Pole 'Ilość na stanie' jest wymagane.");

            RuleFor(a => a.NumberInStock)
                .InclusiveBetween(1, 20).WithMessage("Ilość na stanie musi mieścić się w zakresie od 1 do 20");
        }
    }
}
