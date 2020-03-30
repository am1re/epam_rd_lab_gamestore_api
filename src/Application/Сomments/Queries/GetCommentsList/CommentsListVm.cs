using Application.Common.ViewModels;

namespace Application.Сomments.Queries.GetCommentsList
{
    public class CommentsListVm : EntitiesListVm<CommentLookUpDto>
    {
        public int Depth { get; set; }
    }
}
