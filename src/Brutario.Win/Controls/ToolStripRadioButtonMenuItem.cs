﻿// <copyright file="ToolStripRadioButtonMenuItem.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Win.Controls;

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

public class ToolStripRadioButtonMenuItem : ToolStripMenuItem
{
    public ToolStripRadioButtonMenuItem()
        : base()
    {
        CheckOnClick = true;
    }

    // Enable the item only if its parent item is in the checked state and its Enabled
    // property has not been explicitly set to false.
    public override bool Enabled
    {
        get
        {
            // Use the base value in design mode to prevent the designer from setting
            // the base value to the calculated value.
            return base.Enabled && (
                DesignMode ||
                OwnerItem is not ToolStripMenuItem ownerMenuItem ||
                !ownerMenuItem.CheckOnClick ||
                ownerMenuItem.CheckOnClick);
        }

        set
        {
            base.Enabled = value;
        }
    }

    private bool MouseHoverState { get; set; }

    private bool MouseDownState { get; set; }

    protected override void OnCheckedChanged(EventArgs e)
    {
        base.OnCheckedChanged(e);

        // If this item is no longer in the checked state or if its parent has not yet
        // been initialized, do nothing.
        if (!Checked || Parent == null)
        {
            return;
        }

        // Clear the checked state for all siblings.
        foreach (ToolStripItem item in Parent.Items)
        {
            if (item is ToolStripRadioButtonMenuItem radioItem
                && radioItem != this
                && radioItem.Checked)
            {
                radioItem.Checked = false;

                // Only one item can be selected at a time, so there is no need to continue.
                return;
            }
        }
    }

    protected override void OnClick(EventArgs e)
    {
        // If the item is already in the checked state, do not call the base method,
        // which would toggle the value.
        if (Checked)
        {
            return;
        }

        base.OnClick(e);
    }

    // Let the item paint itself, and then paint the RadioButton where the check mark
    // is normally displayed.
    protected override void OnPaint(PaintEventArgs e)
    {
        if (Image is null)
        {
            // If the client sets the Image property, the selection behavior remains
            // unchanged, but the RadioButton is not displayed and the selection is
            // indicated only by the selection rectangle.
            base.OnPaint(e);
            return;
        }
        else
        {
            // If the Image property is not set, call the base OnPaint method with the
            // CheckState property temporarily cleared to prevent the check mark from
            // being painted.
            var currentState = CheckState;
            CheckState = CheckState.Unchecked;
            base.OnPaint(e);
            CheckState = currentState;
        }

        // Determine the correct state of the RadioButton.
        var buttonState = RadioButtonState.UncheckedNormal;
        if (Enabled)
        {
            if (MouseDownState)
            {
                buttonState = Checked
                    ? RadioButtonState.CheckedPressed
                    : RadioButtonState.UncheckedPressed;
            }
            else if (MouseHoverState)
            {
                buttonState = Checked
                    ? RadioButtonState.CheckedHot
                    : RadioButtonState.UncheckedHot;
            }
            else if (Checked)
            {
                buttonState = RadioButtonState.CheckedNormal;
            }
        }
        else
        {
            buttonState = Checked
                ? RadioButtonState.CheckedDisabled
                : RadioButtonState.UncheckedDisabled;
        }

        // Calculate the position at which to display the RadioButton.
        var offset = (ContentRectangle.Height -
            RadioButtonRenderer.GetGlyphSize(
            e.Graphics, buttonState).Height) / 2;
        var imageLocation = new Point(
            ContentRectangle.Location.X + 4,
            ContentRectangle.Location.Y + offset);

        // Paint the RadioButton.
        RadioButtonRenderer.DrawRadioButton(e.Graphics, imageLocation, buttonState);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        MouseHoverState = true;

        // Force the item to repaint with the new RadioButton state.
        Invalidate();

        base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        MouseHoverState = false;
        base.OnMouseLeave(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        MouseDownState = true;

        // Force the item to repaint with the new RadioButton state.
        Invalidate();

        base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        MouseDownState = false;
        base.OnMouseUp(e);
    }

    // When OwnerItem becomes available, if it is a ToolStripMenuItem with a
    // CheckOnClick property value of true, subscribe to its CheckedChanged event.
    protected override void OnOwnerChanged(EventArgs e)
    {
        if (OwnerItem is ToolStripMenuItem ownerMenuItem
            && ownerMenuItem.CheckOnClick)
        {
            ownerMenuItem.CheckedChanged +=
                new EventHandler(OwnerMenuItem_CheckedChanged);
        }

        base.OnOwnerChanged(e);
    }

    // When the checked state of the parent item changes, repaint the item so that the
    // new Enabled state is displayed.
    private void OwnerMenuItem_CheckedChanged(object? sender, EventArgs e)
    {
        Invalidate();
    }
}
