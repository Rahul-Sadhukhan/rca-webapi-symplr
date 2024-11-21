using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Rca.Data.Entities;
using Rca.Enums;
using Rca.Models;
using Rca.Models.OData;

namespace Rca
{
    public class RcaAutoMapperProfile : Profile
    {
        public RcaAutoMapperProfile()
        {
            RegisterRootCauseAnalysisModel();
            RegisterClassificationEntityToClassificationModelMap();
            RegisterCauseModel();
            RegisterCauseEntityToCauseModelMap();
            RegisterRcaDashboardODataModel();
            RegisterCauseEntityToCauseSelectListModelMap();
            RegisterParticipantODataModelModel();
            RegisterItemODataModelModel();
            RegisterRootCauseAnalysisDetailModel();
            RegisterCreateRootCauseAnalysisModel();
        }

        #region Root Cause Analysis

        private void RegisterRootCauseAnalysisModel()
        {
            CreateMap<RootCauseAnalysis, RootCauseAnalysisModel>()
                .ForMember(dest => dest.RcaId, opt => opt.MapFrom(src => src.RcaId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.RcaName, opt => opt.MapFrom(src => src.RcaName))
                .ForMember(dest => dest.Theme, opt => opt.MapFrom(src => src.Theme))
                .ForMember(dest => dest.IncidentDate, opt => opt.MapFrom(src => src.IncidentDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedByUserId, opt => opt.MapFrom(src => src.CreatedByUserId))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.LastModifiedByUserId, opt => opt.MapFrom(src => src.LastModifiedByUserId))
                .ForMember(dest => dest.LastModifiedDate, opt => opt.MapFrom(src => src.LastModifiedDate))
                .ForAllOtherMembers(x => x.Ignore());
        }

        private void RegisterCreateRootCauseAnalysisModel()
        {
            CreateMap<RootCauseAnalysis, CreateRCAModel>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.RcaName, opt => opt.MapFrom(src => src.RcaName))
                .ForMember(dest => dest.Theme, opt => opt.MapFrom(src => src.Theme))
                .ForMember(dest => dest.IncidentDate, opt => opt.MapFrom(src => src.IncidentDate))
                .ForAllOtherMembers(x => x.Ignore());
        }
        #endregion

        #region Classifications

        private void RegisterClassificationEntityToClassificationModelMap()
        {
            CreateMap<Classification, ClassificationModel>()
               .ForMember(dest => dest.ClassificationName, opt => opt.MapFrom(src => src.ClassificationName))
               .ForMember(dest => dest.ClassificationId, opt => opt.MapFrom(src => src.ClassificationId))
               .ForAllOtherMembers(opt => opt.Ignore());
        }

        #endregion

        #region Causes

        private void RegisterCauseEntityToCauseModelMap()
        {
            CreateMap<CauseModel, Cause>()
                .ForMember(dest => dest.CauseId, opt => opt.MapFrom(src => src.CauseId))
                .ForMember(dest => dest.RCAId, opt => opt.MapFrom(src => src.RCAId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ClassificationId, opt => opt.MapFrom(src => src.ClassificationId == null ? (int)ClassificationType.NoClassification : src.ClassificationId))
                .ForMember(dest => dest.ParentCauseId, opt => opt.MapFrom(src => src.ParentCauseId))
                .ForAllOtherMembers(x => x.Ignore());
        }

        private void RegisterCauseModel()
        {
            CreateMap<Cause, CauseModel>()
                .ForMember(dest => dest.RCAId, opt => opt.MapFrom(src => src.RCAId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CauseId, opt => opt.MapFrom(src => src.CauseId))
                .ForMember(dest => dest.ClassificationId, opt => opt.MapFrom(src => src.ClassificationId))
                .ForMember(dest => dest.ParentCauseId, opt => opt.MapFrom(src => src.ParentCauseId))
                .ForAllOtherMembers(x => x.Ignore());
        }

        private void RegisterRcaDashboardODataModel()
        {
            CreateMap<RCADashboardInfoView, RcaDashboardODataModel>()
                .ForMember(dest => dest.RcaId, opt => opt.MapFrom(src => src.RcaId))
                .ForMember(dest => dest.IncidentDate, opt => opt.MapFrom(src => src.IncidentDate))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ProblemStatement, opt => opt.MapFrom(src => src.ProblemStatement))
                .ForMember(dest => dest.Theme, opt => opt.MapFrom(src => src.Theme))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
                .ForMember(dest => dest.LinkedItems, opt => opt.MapFrom(src => src.LinkedItems))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedByUserId, opt => opt.MapFrom(src => src.CreatedByUserId))
                .ForAllOtherMembers(x => x.Ignore());
        }

        private void RegisterCauseEntityToCauseSelectListModelMap()
        {
            CreateMap<Cause, CauseSelectListModel>()
                .ForMember(dest => dest.CauseId, opt => opt.MapFrom(src => src.CauseId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ParentCauseId, opt => opt.MapFrom(src => src.ParentCauseId))
                .ForAllOtherMembers(x => x.Ignore());
        }

        #endregion

        private void RegisterParticipantODataModelModel()
        {
            CreateMap<Participant, ParticipantODataModel>()
                .ForMember(dest => dest.ParticipantId, opt => opt.MapFrom(src => src.ParticipantId))
                .ForMember(dest => dest.ParticipantName, opt => opt.MapFrom(src => src.ParticipantName))
                .ForAllOtherMembers(x => x.Ignore());
        }

        private void RegisterRootCauseAnalysisDetailModel()
        {
            CreateMap<List<RootCauseAnalysisDetails>, RootCauseAnalysisDetailModel>()
               .AfterMap(
                    (entity, model) =>
                    {
                        if (entity == null || !entity.Any())
                        {
                            return;
                        }

                        var detailModel = entity.FirstOrDefault();
                        var validParticipants = entity
                            .Where(x => x.ParticipantId > 0)?
                            .GroupBy(x => x.ParticipantId)
                            .Select(g => g.First())
                            .Select(x => new ParticipantModel
                            {
                                ParticipantId = x.ParticipantId,
                                Name = x.ParticipantName,
                            }).ToList();

                        var validItems = entity
                            .Where(x => x.ItemId > 0)?
                            .GroupBy(x => x.ItemId)
                            .Select(g => g.First())
                            .Select(x => new LinkedItemModel
                            {
                                ItemId = x.ItemId,
                                Name = x.ItemName,
                                IsReadPermissionEnabled = x.IsReadPermissionEnabled ?? false,
                            }).ToList();

                        if (detailModel != null)
                        {
                            model.RcaId = detailModel.RcaId;
                            model.Description = detailModel.Description;
                            model.RcaName = detailModel.RcaName;
                            model.Status = detailModel.Status;
                            model.IncidentDate = detailModel.IncidentDate;
                            model.Theme = detailModel.Theme;
                            model.Participants = validParticipants;
                            model.LinkedItems = validItems;
                        }
                    })
                .ForAllOtherMembers(x => x.Ignore());
        }

        private void RegisterItemODataModelModel()
        {
            CreateMap<Item, ItemODataModel>()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
