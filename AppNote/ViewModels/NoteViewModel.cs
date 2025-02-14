﻿using AppNote.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppNote.ViewModels
{
    public partial class NoteViewModel : INotifyPropertyChanged
    {
        // Fields
        private string _noteTitle;
        private string _noteDescription;
        private Note _selectedNote;

        // Get and Set
        public string NoteTitle
        {
            get => _noteTitle;
            set
            {
                if (_noteTitle != value)
                {
                    _noteTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NoteDescription
        {
            get => _noteDescription;
            set
            {
                if (_noteDescription != value)
                {
                    _noteDescription = value;
                    OnPropertyChanged();
                }
            }
        }

        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                if(_selectedNote != value)
                {
                    _selectedNote = value;
                    NoteTitle = _selectedNote.Title;
                    NoteDescription = _selectedNote.Description;  // set from list to ui
                    OnPropertyChanged();
                }
            }
        }

        // Property
        public ObservableCollection<Note> NotesCollection { get; set; }
        public ICommand AddNoteCommand { get; }
        public ICommand EditNoteCommand { get; }
        public ICommand RemoveNoteCommand { get; }
        public NoteViewModel()
        {
            NotesCollection = new ObservableCollection<Note>();
            AddNoteCommand = new Command(AddNote);
            RemoveNoteCommand = new Command(RemoveNote);
            EditNoteCommand = new Command(EditNote);
        }

        private void EditNote(object obj)
        {
            if(SelectedNote != null)
            {
                foreach(Note note in NotesCollection.ToList())
                {
                    if(note == SelectedNote)
                    { 
                        // Set new note
                        var newNote = new Note
                        {
                            Id = note.Id,
                            Title = NoteTitle,
                            Description = NoteDescription,
                        };
                        // remove note
                        NotesCollection.Remove(note);
                        NotesCollection.Add(newNote);
                        NoteTitle = string.Empty;
                        NoteDescription = string.Empty;
                    }
                }
            }
        }

        private void RemoveNote(object obj)
        {
            if(SelectedNote != null)
            {
                NotesCollection.Remove(SelectedNote);
                // Rest values
                NoteTitle = string.Empty;
                NoteDescription = string.Empty;
            }
        }

        private void AddNote(object obj)
        {
            // Generate a unique ID for the new person
            int newId = NotesCollection.Count > 0 ? NotesCollection.Max(p => p.Id) + 1 : 1;
            // Set New Note
            var note = new Note
            {
                Id = newId,
                Title = NoteTitle,
                Description = NoteDescription,
            };
            NotesCollection.Add(note);

            // Rest Values
            NoteTitle = string.Empty;
            NoteDescription = string.Empty;
        }

        // PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
