using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models.DTOS;

namespace Todo.Models.Configurations
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig() {

            // TodoItem
            CreateMap<TodoItem, TodoItemDTO>()
                .ForMember(_ => _.Id, opt => opt.MapFrom(_ => _.Id))
                .ForMember(_ => _.Title, opt => opt.MapFrom(_ => _.Title))
                .ForMember(_ => _.Description, opt => opt.MapFrom(_ => _.Description))
                .ForMember(_ => _.CreatedAt, opt => opt.MapFrom(_ => _.CreatedAt))
                .ForMember(_ => _.IsTasked, opt => opt.MapFrom(_ => _.IsTasked))
                .ForMember(_ => _.IsImportant, opt => opt.MapFrom(_ => _.IsImportant))
                .ForMember(_ => _.IsCompleted, opt => opt.MapFrom(_ => _.IsCompleted))
                .ForMember(_ => _.IsTodayTodo, opt => opt.MapFrom(_ => _.IsTodayTodo))
                .ForMember(_ => _.ListId, opt => opt.MapFrom(_ => _.ListId))
                .ReverseMap();

            // TodoList
            CreateMap<TodoList, TodoListDTO>()
                .ForMember(_ => _.Id, opt => opt.MapFrom(_ => _.Id))
                .ForMember(_ => _.ListName, opt => opt.MapFrom(_ => _.ListName))
                .ReverseMap();
            
        }
    }
}
