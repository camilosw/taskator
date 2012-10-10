class Proyecto < ActiveRecord::Base
  attr_accessible :nombre, :cliente_id
  belongs_to :cliente

  validates :nombre, presence: true
  validates :cliente_id, presence: true
end

# == Schema Information
#
# Table name: proyectos
#
#  id         :integer          not null, primary key
#  cliente_id :integer
#  nombre     :string(255)      not null
#  created_at :datetime         not null
#  updated_at :datetime         not null
#

