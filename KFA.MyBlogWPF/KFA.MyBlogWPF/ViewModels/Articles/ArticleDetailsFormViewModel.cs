using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Articles
{
    public class ArticleDetailsFormViewModel : ViewModelBase
    {
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
        private string content;
        public string Content
        {
            get { return content; }
            set 
            { 
                content = value;
                OnPropertyChanged(nameof(Content));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
        private string authorId;
        public string AuthorId
        {
            get { return authorId; }
            set
            {
                content = value;
                OnPropertyChanged(nameof(authorId));
            }
        }
        private string shortName;
        public string ShortName
        {
            get { return shortName; }
            set
            {
                content = value;
                OnPropertyChanged(nameof(shortName));
            }
        }
        private DateTime articleDate;
        public DateTime ArticleDate
        {
            get { return articleDate; }
            set
            {
                articleDate = value;
                OnPropertyChanged(nameof(ArticleDate));
            }
        }
        private IEnumerable<Tag> tags;
        public IEnumerable<Tag> Tags
        {
            get { return tags; }
            set
            {
                tags = value;
                OnPropertyChanged(nameof(Tags));
            }
        }
        private IEnumerable<TagAssignment> tagAssignments;
        public IEnumerable<TagAssignment> TagAssignments
        {
            get { return tagAssignments; }
            set
            {
                tagAssignments = value;
                OnPropertyChanged(nameof(TagAssignments));
            }
        }

        private IEnumerable<Tag> allTags;
        public bool CanSubmit => !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Content);
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public ArticleDetailsFormViewModel(ICommand submitCommand, ICommand cancelCommand)
        {
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }
        public void InitTags(List<Tag> allTags, List<Tag> articleTags)
        {
            if(allTags == null || !allTags.Any())
            {
                TagAssignments = new List<TagAssignment>();
                return;
            }
            this.allTags = allTags;
            TagAssignments = allTags.Select(tag => new TagAssignment()
            {
                Tag = tag,
                IsAssigned = articleTags?.Any(x => x.Id == tag.Id) ?? false
            }).ToList();
        }
        public List<Tag> GetSelectedTags()
        {
            return TagAssignments?.Where(at => at.IsAssigned).Select(ta => ta.Tag).ToList() ?? new List<Tag>();
        }
    }
}
