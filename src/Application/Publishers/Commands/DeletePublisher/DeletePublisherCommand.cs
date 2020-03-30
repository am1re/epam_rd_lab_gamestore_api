﻿using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Publishers.Commands.DeletePublisher
{
    public class DeletePublisherCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeletePublisherCommand>
        {
            private readonly IGameStoreDbContext _context;

            public DeleteCategoryCommandHandler(IGameStoreDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Publishers.FindAsync(request.Id);

                if (entity == null)
                { 
                    throw new NotFoundException(nameof(Publisher), request.Id);
                }

                _context.Publishers.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
