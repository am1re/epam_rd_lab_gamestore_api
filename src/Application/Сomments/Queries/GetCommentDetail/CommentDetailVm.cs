using Application.Common.ViewModels;

namespace Application.Сomments.Queries.GetCommentDetail
{
    public class CommentDetailVm : EntityDetailVm<CommentDetailDto>
    {
        public int Depth { get; set; }
    }
}