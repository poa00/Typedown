﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.UI.Xaml;

namespace Typedown.Universal.Models
{
    public class HistoryModel
    {
        public string Text { get; set; } = null;
        public JToken Cursor { get; set; } = null;
    }

    public class ContentHistory : INotifyPropertyChanged
    {
        const int deep = 100;
        readonly List<HistoryModel> histories = new();
        HistoryModel pending = new();
        int index = -1;
        private readonly DispatcherTimer commitTimer = new();

        public bool Undoable { get; set; }
        public bool Redoable { get; set; }
        public bool IsPending { get => pending.Text != null && pending.Cursor != null; }

        public ContentHistory()
        {
            commitTimer.Tick += (s, e) => CommitPending();
        }

        public HistoryModel Undo()
        {
            try
            {
                if (index > 0 || (index == 0 && IsPending))
                {
                    CommitPending();
                    index--;
                    Redoable = true;
                    Undoable = index > 0;
                    return histories[index];
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            return null;
        }

        public HistoryModel Redo()
        {
            try
            {
                if (index < histories.Count - 1)
                {
                    commitTimer.Stop();
                    pending = new();
                    index++;
                    Redoable = index < histories.Count - 1;
                    Undoable = true;
                    return histories[index];
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            return null;
        }

        public void ClearHistory()
        {
            try
            {
                histories.Clear();
                commitTimer.Stop();
                pending = new();
                index = -1;
                Redoable = false;
                Undoable = false;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        public void CommitPending()
        {
            try
            {
                if (!IsPending) return;
                commitTimer.Stop();
                histories.RemoveRange(index + 1, histories.Count - (index + 1));
                histories.Add(pending);
                if (histories.Count > deep)
                {
                    histories.RemoveAt(0);
                }
                else
                {
                    index++;
                }
                pending = new();
                Redoable = false;
                Undoable = index > 0;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void ResetTimer()
        {
            commitTimer.Stop();
            commitTimer.Interval = TimeSpan.FromSeconds(3);
            commitTimer.Start();
        }

        public void CursorChange(JToken cursor)
        {
            try
            {
                if (pending.Text == null && index > -1)
                {
                    histories[index].Cursor = cursor;
                    return;
                }
                if (cursor == null)
                {
                    return;
                }
                if (IsPending && pending.Cursor["focus"]["line"].ToObject<int>() != cursor["focus"]["line"].ToObject<int>())
                {
                    pending.Cursor = cursor;
                    CommitPending();
                    return;
                }
                pending.Cursor = cursor;
                if (pending.Text != null && histories.Count == 0)
                {
                    CommitPending();
                    return;
                }
                StateChange();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        public void ContentChange(string content)
        {
            try
            {
                content = content.TrimEnd('\r', '\n');
                if ((pending.Text != null && pending.Text == content) ||
                    (pending.Text == null && index > -1 && histories[index].Text == content))
                {
                    return;
                }
                pending.Text = content;
                if (pending.Cursor != null && histories.Count == 0)
                {
                    CommitPending();
                    return;
                }
                StateChange();
                ResetTimer();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void StateChange()
        {
            Redoable = index < histories.Count - 1;
            Undoable = index > 0 || (index == 0 && pending.Text != null && pending.Cursor != null);
        }

        public void InitHistory(string content)
        {
            ClearHistory();
            CursorChange(JToken.FromObject(new
            {
                focus = new
                {
                    line = 0,
                    ch = 0
                },
                anchor = new
                {
                    line = 0,
                    ch = 0
                }
            }));
            ContentChange(content);
        }
#pragma warning disable CS0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067
    }

}