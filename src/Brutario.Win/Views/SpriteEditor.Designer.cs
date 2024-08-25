﻿// <copyright file="SpriteEditor.Designer.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Win.Views
{
    partial class SpriteEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.spriteEditorDialog = new Brutario.Win.Dialogs.SpriteEditorDialog(this.components);
            //
            // spriteEditorDialog
            //
            this.spriteEditorDialog.ShowHelp = false;
            this.spriteEditorDialog.Title = "Sprite Editor";
            this.spriteEditorDialog.AreaSpriteCommandChanged += new System.EventHandler(this.SpriteEditorDialog_AreaSpriteCommandChanged);

        }

        #endregion

        private Dialogs.SpriteEditorDialog spriteEditorDialog;
    }
}
